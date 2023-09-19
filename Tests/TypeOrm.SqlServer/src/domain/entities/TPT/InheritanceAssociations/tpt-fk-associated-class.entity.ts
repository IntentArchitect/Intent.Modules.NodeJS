import { TptFkBaseClass } from './tpt-fk-base-class.entity';
import { Entity, PrimaryGeneratedColumn, Column, ManyToOne } from 'typeorm';

@Entity('tpt_fk_associated_class')
export class TptFkAssociatedClass {
  @PrimaryGeneratedColumn('uuid')
  id: string;

  @Column()
  associatedField: string;

  @Column()
  fkBaseClassCompositeKeyA: string;

  @Column()
  fkBaseClassCompositeKeyB: string;

  @ManyToOne(() => TptFkBaseClass, { cascade: ['insert', 'update'], nullable: false })
  fkBaseClass: TptFkBaseClass;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}