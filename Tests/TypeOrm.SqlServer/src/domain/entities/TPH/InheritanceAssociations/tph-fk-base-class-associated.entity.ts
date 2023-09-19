import { TphFkBaseClass } from './tph-fk-base-class.entity';
import { Entity, PrimaryGeneratedColumn, Column, ManyToOne } from 'typeorm';

@Entity('tph_fk_base_class_associated')
export class TphFkBaseClassAssociated {
  @PrimaryGeneratedColumn('uuid')
  id: string;

  @Column()
  associatedField: string;

  @Column()
  fkBaseClassCompositeKeyA: string;

  @Column()
  fkBaseClassCompositeKeyB: string;

  @ManyToOne(() => TphFkBaseClass, { cascade: ['insert', 'update'], nullable: false })
  fkBaseClass: TphFkBaseClass;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}