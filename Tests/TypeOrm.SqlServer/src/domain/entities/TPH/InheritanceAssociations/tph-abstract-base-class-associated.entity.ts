import { TphAbstractBaseClass } from './tph-abstract-base-class.entity';
import { Entity, PrimaryGeneratedColumn, Column, ManyToOne } from 'typeorm';

@Entity('tph_abstract_base_class_associated')
export class TphAbstractBaseClassAssociated {
  @PrimaryGeneratedColumn('uuid')
  id: string;

  @Column()
  associatedField: string;

  @Column()
  abstractBaseClassId: string;

  @ManyToOne(() => TphAbstractBaseClass, { cascade: ['insert', 'update'], nullable: false })
  abstractBaseClass: TphAbstractBaseClass;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}