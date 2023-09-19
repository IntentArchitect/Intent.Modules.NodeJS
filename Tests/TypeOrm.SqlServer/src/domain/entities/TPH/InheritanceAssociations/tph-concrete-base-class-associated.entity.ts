import { TphConcreteBaseClass } from './tph-concrete-base-class.entity';
import { Entity, PrimaryGeneratedColumn, Column, ManyToOne } from 'typeorm';

@Entity('tph_concrete_base_class_associated')
export class TphConcreteBaseClassAssociated {
  @PrimaryGeneratedColumn('uuid')
  id: string;

  @Column()
  associatedField: string;

  @Column()
  concreteBaseClassId: string;

  @ManyToOne(() => TphConcreteBaseClass, { cascade: ['insert', 'update'], nullable: false })
  concreteBaseClass: TphConcreteBaseClass;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}